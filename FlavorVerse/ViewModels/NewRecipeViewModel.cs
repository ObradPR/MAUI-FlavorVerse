using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Extensions;
using FlavorVerse.Interfaces;
using FlavorVerse.Services;
using FlavorVerse.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FlavorVerse.ViewModels
{
    public class NewRecipeViewModel
    {
        private readonly IRecipeService _recipeService;

        public MProp<string> Title { get; set; } = new MProp<string>();

        public MProp<string> Description { get; set; } = new MProp<string>();

        public MProp<string> CookingTime { get; set; } = new MProp<string>();

        public MProp<int> Servings { get; set; } = new MProp<int>();

        public MProp<string> Instructions { get; set; } = new MProp<string>();

        public MProp<int?> Protein { get; set; } = new MProp<int?>();

        public MProp<int?> Carbohydrates { get; set; } = new MProp<int?>();

        public MProp<int?> Fat { get; set; } = new MProp<int?>();

        public MProp<int?> Fiber { get; set; } = new MProp<int?>();

        public bool? GlutenFree { get; set; }

        public bool? DairyFree { get; set; }

        public bool? Vegetarian { get; set; }

        public bool? Vegan { get; set; }

        // Dropdown Selections
        public ObservableCollection<string> MealTypes { get; set; } = [];

        public MProp<string> SelectedMealType { get; set; } = new MProp<string>();

        public ObservableCollection<string> DifficultyCookings { get; set; } = [];

        public MProp<string> SelectedDifficultyCooking { get; set; } = new MProp<string>();

        public ObservableCollection<string> Cuisines { get; set; } = [];

        public MProp<string> SelectedCuisine { get; set; } = new MProp<string>();

        public ObservableCollection<string> Categories { get; set; } = [];

        public MProp<string> SelectedCategory { get; set; } = new MProp<string>();

        // Image
        public MProp<IFormFile> Image { get; set; } = new MProp<IFormFile>();

        // Command for picking an image
        public ICommand UploadImageCommand { get; set; }

        // Submit command
        public ICommand SubmitCommand { get; set; }

        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();

        public NewRecipeViewModel()
        {
            _recipeService = new RecipeService();
            LoadPickers();

            UploadImageCommand = new Command(UploadImage);
            SubmitCommand = new Command(SubmitRecipe);

            Title.OnChange = Validate;
            Image.OnChange = Validate;
            Description.OnChange = Validate;
            CookingTime.OnChange = Validate;
            Servings.OnChange = Validate;
            Instructions.OnChange = Validate;
            SelectedMealType.OnChange = Validate;
            SelectedDifficultyCooking.OnChange = Validate;
            SelectedCuisine.OnChange = Validate;
            SelectedCategory.OnChange = Validate;
        }

        private void Validate()
        {
            var validator = new AddRecipeValidtion();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                Title.Error = result.GetError(nameof(Title));
                Image.Error = result.GetError(nameof(Image));
                Description.Error = result.GetError(nameof(Description));
                CookingTime.Error = result.GetError(nameof(CookingTime));
                Servings.Error = result.GetError(nameof(Servings));
                Instructions.Error = result.GetError(nameof(Instructions));
                SelectedMealType.Error = result.GetError(nameof(SelectedMealType));
                SelectedDifficultyCooking.Error = result.GetError(nameof(SelectedDifficultyCooking));
                SelectedCuisine.Error = result.GetError(nameof(SelectedCuisine));
                SelectedCategory.Error = result.GetError(nameof(SelectedCategory));

                ButtonEnabled.Value = false;
            }
            else
            {
                Title.Error = string.Empty;
                Image.Error = string.Empty;
                Description.Error = string.Empty;
                CookingTime.Error = string.Empty;
                Servings.Error = string.Empty;
                Instructions.Error = string.Empty;
                SelectedMealType.Error = string.Empty;
                SelectedDifficultyCooking.Error = string.Empty;
                SelectedCuisine.Error = string.Empty;
                SelectedCategory.Error = string.Empty;

                ButtonEnabled.Value = true;
            }
        }

        private async void UploadImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                // Convert the picked file into an IFormFile object.
                // In Maui, you'll need to adapt it based on the file stream.
                var stream = await result.OpenReadAsync();
                var formFile = new FormFile(stream, 0, stream.Length, result.FileName, result.FileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = result.ContentType
                };

                // Use MProp to update the Image, which automatically triggers OnPropertyChanged
                Image.Value = formFile;
            }
        }

        private async void SubmitRecipe()
        {
            var newRecipe = new AddRecipeDto
            {
                Title = Title.Value,
                Description = Description.Value,
                CookingTime = CookingTime.Value,
                Servings = Servings.Value,
                Instructions = Instructions.Value,
                Protein = Protein.Value,
                Carbohydrates = Carbohydrates.Value,
                Fat = Fat.Value,
                Fiber = Fiber.Value,
                GlutenFree = GlutenFree,
                DairyFree = DairyFree,
                Vegetarian = Vegetarian,
                Vegan = Vegan,
                MealTypeId = MealTypes.IndexOf(SelectedMealType.Value) + 1,
                DifficultyCookingId = DifficultyCookings.IndexOf(SelectedDifficultyCooking.Value) + 1,
                MainCuisineId = Cuisines.IndexOf(SelectedCuisine.Value) + 1,
                MainCategoryId = Categories.IndexOf(SelectedCategory.Value) + 1,
                Image = Image.Value
            };

            try
            {
                await _recipeService.AddAsync(newRecipe);
                if (newRecipe.Image != null)
                {
                    await SaveFileToLocalFolder(newRecipe.Image);
                }

                ResetFields();

                App.Current.MainPage = new MainPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ResetFields()
        {
            Title.Value = string.Empty;
            Description.Value = string.Empty;
            CookingTime.Value = string.Empty;
            Servings.Value = 0;
            Instructions.Value = string.Empty;
            Protein.Value = 0;
            Carbohydrates.Value = 0;
            Fat.Value = 0;
            Fiber.Value = 0;

            GlutenFree = false;
            DairyFree = false;
            Vegetarian = false;
            Vegan = false;

            SelectedMealType.Value = string.Empty;
            SelectedDifficultyCooking.Value = string.Empty;
            SelectedCuisine.Value = string.Empty;
            SelectedCategory.Value = string.Empty;

            Image.Value = null;
        }

        private async Task SaveFileToLocalFolder(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null.");

            // Get the path to the Resources/Images folder within the current project directory
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var projectRootPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\"));
            var folderPath = Path.Combine(projectRootPath, "Resources", "Images");

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var formattedFileName = Path.GetFileName(file.FileName).ToLower().Replace(" ", "_");

            var filePath = Path.Combine(folderPath, formattedFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private void LoadPickers()
        {
            MealTypes = new ObservableCollection<string>
            {
                "1: Breakfast",
                "2: Lunch",
                "3: Dinner",
                "4: Snack",
                "5: Dessert"
            };

            // Populating Categories based on seed data
            Categories = new ObservableCollection<string>
            {
                "1: Main Courses",
                "2: Appetizers",
                "3: Salads",
                "4: Desserts",
                "5: Seafood",
                "6: Pasta",
                "7: Soup",
                "8: Dips",
                "9: Greek Salad",
                "10: Fruit Salad",
                "11: Cakes",
                "12: Cookies",
                "13: Grilled",
                "14: Stir Fry",
                "15: Sandwiches",
                "16: Stews",
                "17: Burgers",
                "18: Barbecue",
                "19: Pizza",
                "20: Wraps",
                "21: Finger Foods",
                "22: Spring Rolls",
                "23: Mixed Greens",
                "24: Coleslaw",
                "25: Mousse",
                "26: Pies",
                "27: Chicken Dishes",
                "28: Tacos",
                "29: Beef Dishes"
            };

            // Populating Cuisines based on seed data
            Cuisines = new ObservableCollection<string>
            {
                "1: Italian",
                "2: Chinese",
                "3: Mexican",
                "4: Japanese",
                "5: Indian",
                "6: French",
                "7: Thai",
                "8: Spanish",
                "9: Greek",
                "10: Lebanese",
                "11: Turkish",
                "12: Korean",
                "13: Brazilian",
                "14: Moroccan",
                "15: Vietnamese",
                "16: Ethiopian"
            };

            // Populating DifficultyLevels based on the enum
            DifficultyCookings = new ObservableCollection<string>
            {
                "1: Easy",
                "2: Medium",
                "3: Hard",
                "4: Expert"
            };
        }
    }
}